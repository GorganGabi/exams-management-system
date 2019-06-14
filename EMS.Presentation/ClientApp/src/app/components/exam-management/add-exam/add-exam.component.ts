import {Component, OnInit} from '@angular/core';
import {Exam} from '../../../models/exam';
import {ExamService} from '../../../services/exam.service';
import {Router} from '@angular/router';
import {ProfessorService} from 'src/app/services/professor.service';
import {Course} from 'src/app/models/course';

@Component({
  selector: 'app-add-exam',
  templateUrl: './add-exam.component.html',
  styleUrls: ['./add-exam.component.css']
})
export class AddExamComponent implements OnInit {
  type: string;
  room: string;
  date: Date;
  exam: Exam;
  course: Course;

  constructor(
    private examService: ExamService,
    private professorService: ProfessorService,
    private route: Router) {
  }

  ngOnInit() {
  }

  createExam() {
    this.exam = new Exam();
    this.exam.type = this.type;
    this.exam.room = this.room;
    this.exam.date = this.date;
    this.exam.course = new Course();
    this.professorService.getProfessorCourses(localStorage.getItem('userID'))
      .subscribe(courses => {
        this.exam.course.id = courses[0].id;
        this.examService.createExam(this.exam)
          .subscribe(exam => {
            this.exam = exam;
            this.route.navigate(['/exams']);
          });
      });
  }
}
