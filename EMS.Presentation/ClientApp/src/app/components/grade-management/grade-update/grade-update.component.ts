import {Component, Input, OnInit} from '@angular/core';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {GradeService} from '../../../services/grade.service';
import {Grade} from '../../../models/grade';
import {StudentService} from '../../../services/student.service';
import {Student} from '../../../models/student';

@Component({
  selector: 'app-grade-update',
  templateUrl: './grade-update.component.html',
  styleUrls: ['./grade-update.component.css']
})
export class GradeUpdateComponent implements OnInit {
  @Input() grade: Grade;

  constructor(private modalService: NgbModal,
              private gradeService: GradeService,
              private studentService: StudentService) {
  }

  ngOnInit() {
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

  update() {
    this.gradeService.updateGrade(this.grade)
      .subscribe();

    const updateStudent: Student = {
      id: this.grade.studentId,
      name: this.grade.studentName
    };
    this.studentService.updateStudent(updateStudent)
      .subscribe();
}
}
