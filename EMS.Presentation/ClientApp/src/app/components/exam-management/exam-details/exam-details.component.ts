import {Component, OnInit, Input} from '@angular/core';
import {Exam} from '../../../models/exam';
import {ExamService} from '../../../services/exam.service';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {Location} from '@angular/common';

@Component({
  selector: 'app-exam-details',
  templateUrl: './exam-details.component.html',
  styleUrls: ['./exam-details.component.css']
})
export class ExamDetailsComponent implements OnInit {
  exam: Exam;
  isEditable: boolean = false;

  constructor(
    private examService: ExamService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit() {
    this.getExam()
  }

  goBack() {
    this.location.back();
  }

  getExam() {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.examService.getExam(map.get('id')))
    ).subscribe(exam => this.exam = exam);
  }

  updateExam(): void {
    this.examService.updateExam(this.exam)
      .subscribe(() => this.goBack());
  }

  edit(): void {
    this.isEditable = true;
  }

  saveChanges(): void {
    this.updateExam();
  }

}
