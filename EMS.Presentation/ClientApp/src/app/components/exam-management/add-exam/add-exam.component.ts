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
  title: string;
  courses: Course[];

  constructor(
    private examService: ExamService,
    private professorService: ProfessorService,
    private route: Router) {
  }

  ngOnInit() {
    this.professorService.getProfessorCourses(localStorage.getItem('userID'))
      .subscribe(courses => this.courses = courses);
  }

  createExam() {
    this.exam = new Exam();
    this.exam.type = this.type;
    this.exam.room = this.room;
    this.exam.date = this.date;
    this.exam.course = new Course();
    this.professorService.getProfessorCourses(localStorage.getItem('userID'))
      .subscribe(courses => {
        for (let i = 0; i < courses.length; i++) {
          if (courses[i].title === this.title) {
            this.exam.course.id = courses[i].id;
            break;
          }
        }

        this.examService.createExam(this.exam)
          .subscribe(exam => {
            this.exam = exam;
            this.route.navigate(['/exams']);
          });
      });
  }

  onSelectTitle(title: string) {
    this.title = title;
  }
}
