import {Component, Input, OnInit} from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {GradeService} from "../../../services/grade.service";
import {Location} from "@angular/common";
import {Grade} from "../../../models/grade";
import * as G from "glob";

@Component({
  selector: 'app-grade-update',
  templateUrl: './grade-update.component.html',
  styleUrls: ['./grade-update.component.css']
})
export class GradeUpdateComponent implements OnInit {
  closeResult: string;
  @Input() grade: Grade;

  constructor(private modalService: NgbModal,
              private gradeService: GradeService) {
  }

  ngOnInit() {
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

  update() {
    this.gradeService.updateGrade(this.grade)
      .subscribe();
  }
}
