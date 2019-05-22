import { Component, OnInit } from '@angular/core';
import { Exam } from '../exam';
import { ExamService } from '../exam.service';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})
export class ExamsComponent implements OnInit {
  exams: Exam[];

  constructor(private examService: ExamService) { }

  ngOnInit() {
    this.getExams();
  }

  getExams(){
    this.examService.getExams()
    .subscribe(exams => this.exams = exams);
  }

}
