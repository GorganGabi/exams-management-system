import { Component, OnInit } from '@angular/core';
import { Exam } from '../../exam';
import { ExamService } from '../../exam.service';
import { Router } from '@angular/router';
import { ProfessorService } from 'src/app/professor.service';
import { Course } from 'src/app/course';

@Component({
  selector: 'app-add-exam',
  templateUrl: './add-exam.component.html',
  styleUrls: ['./add-exam.component.css']
})
export class AddExamComponent implements OnInit {
  type: string;
  room: string;
  date: string;
  exam: Exam;
  course: Course;

  constructor(
    private examService: ExamService,
    private professorService: ProfessorService,
    private route: Router) { }

  ngOnInit() {
  }

  createExam() {
    this.exam = new Exam();
    this.exam.type = this.type;
    this.exam.room = this.room;
    this.exam.date = this.date;
    this.professorService.getProfessorCourses(localStorage.getItem('userID'))
      .subscribe(courses => {
          this.exam.courseId = courses[0].id,
          this.examService.createExam(this.exam)
            .subscribe(exam => {
              this.exam = exam,
                this.route.navigate(['/exams'])
            });
      });
  }
}
