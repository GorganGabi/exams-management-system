import {Component, OnInit} from '@angular/core';
import {Exam} from '../../../models/exam';
import {ExamService} from '../../../services/exam.service';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {switchMap} from 'rxjs/operators';

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
  role: string;
  url: string;
  myExam = false;

  constructor(
    private examService: ExamService,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.role = localStorage.getItem('userID');
    this.getExam();
  }

  getExam() {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.examService.getExam(map.get('id')))
    ).subscribe(exam => {
      this.exam = exam;
      const imagePathSplit = this.exam.imagePath.split('/');
      this.imageName = imagePathSplit[imagePathSplit.length - 1];
      this.url = `http://localhost:11111/api/v1/students//exams/${this.exam.id}`;
      console.log(exam);
      for (let i = 0; i < this.exam.professorIds.length; i++) {
        if (this.exam.professorIds[i] === this.role) {
          this.myExam = true;
          break;
        }
      }
    });
  }

  updateExam(): void {
    this.examService.updateExam(this.exam)
      .subscribe(() => this.isEditable = false);
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

  formatDate(date: Date): string {
    date = new Date(date);
    const monthNames = [
      'Ianuarie', 'Februarie', 'Martie',
      'Aprilie', 'Mai', 'Iunie', 'Iulie',
      'August', 'Septembrie', 'Octombrie',
      'Noiembrie', 'Decembrie'
    ];
    const day = date.getDate();
    const monthIndex = date.getMonth();
    const year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
  }

  formatTime(date: Date): string {
    date = new Date(date);
    const hours = date.getHours();
    const minutes = date.getMinutes();

    return (hours < 10 ? '0' + hours : hours) + ':' + ((minutes.toString()) === '0' ? '00' : minutes);
  }
}
