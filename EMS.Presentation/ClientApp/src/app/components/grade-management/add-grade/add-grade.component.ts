import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {GradeService} from '../../../services/grade.service';
import {ActivatedRoute, Router} from '@angular/router';
import {StudentService} from '../../../services/student.service';

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

  createGrade() {
    this.grade = new Grade();
    this.grade.value = this.value;
    this.grade.examId = this.route.snapshot.paramMap.get('id');
    this.studentService.getStudentsByName(this.studentName)
      .subscribe(students => {
        this.grade.student.id = students[0].id;
        this.gradeService.createGrade(this.grade)
          .subscribe(() => {
            this.router.navigate([`/exams/${this.grade.examId}/grades/`]);
          });
      });
  }

  setName(name: string) {
    this.studentName = name;
  }
}
