import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {GradeService} from '../../../services/grade.service';
import {ActivatedRoute, Router} from '@angular/router';
import {StudentService} from '../../../services/student.service';
import {catchError} from "rxjs/operators";
import * as HTTPStatusCodes from "http-status-codes";
import {throwError as observableThrowError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-add-grade',
  templateUrl: './add-grade.component.html',
  styleUrls: ['./add-grade.component.css']
})
export class AddGradeComponent implements OnInit {
  value: number;
  studentName: string;
  grade: Grade;

  constructor(private gradeService: GradeService,
              private route: ActivatedRoute,
              private studentService: StudentService,
              private router: Router) {
  }

  ngOnInit() {
  }

  static errorHandler(error: HttpErrorResponse) {
    if (error.status === HTTPStatusCodes.UNPROCESSABLE_ENTITY) {
      alert('Studentul are deja notă');
    }
    return observableThrowError(error.message);
  }

  setName(name: string) {
    this.studentName = name;
  }

  createGrade() {
    this.studentService.getStudentsByName(this.studentName).pipe(
      catchError(AddGradeComponent.errorHandler)
    )
      .subscribe(students => {
        if (!this.value) {
          alert('Adauga nota');
          return;
        }
        const gradeCreatingModel = {
          value: this.value,
          examId: this.route.snapshot.paramMap.get('id'),
          studentId: students[0].id
        };
        this.gradeService.createGrade(gradeCreatingModel)
          .subscribe(() => {
              console.log('s-a creat nota');
              this.router.navigate([`/exams/${this.route.snapshot.paramMap.get('id')}/grades/`]);
            },
            error => {
              if (error.status === HTTPStatusCodes.UNPROCESSABLE_ENTITY) {
                alert('Studentul are deja nota');
              }
            });
      });
  }
}
