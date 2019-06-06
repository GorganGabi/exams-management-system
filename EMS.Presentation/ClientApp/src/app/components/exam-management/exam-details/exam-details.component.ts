import {Component, OnInit} from '@angular/core';
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
  isEditable = false;
  selectedFile = null;
  imageName: string;

  constructor(
    private examService: ExamService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit() {
    this.getExam();
  }

  goBack() {
    this.location.back();
  }

  getExam() {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.examService.getExam(map.get('id')))
    ).subscribe(exam => {
      this.exam = exam;
      const imagePathSplit = this.exam.imagePath.split('/');
      this.imageName = imagePathSplit[imagePathSplit.length - 1];
    });
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

  uploadFile(event) {
    this.selectedFile = event.target.files[0];
    this.exam.imagePath = `../../../../assets/${this.exam.course.title}/${this.selectedFile.name}`;
  }
}
