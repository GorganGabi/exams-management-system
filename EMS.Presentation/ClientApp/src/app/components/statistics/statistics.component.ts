import {Component, OnInit} from '@angular/core';
import {ExamService} from '../../services/exam.service';
import {Exam} from '../../models/exam';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {
  selectedType: string;
  selectedExamTitle: string;
  selectedExam: Exam;
  types: string[];
  exams: Exam[];
  examsByType: Exam[];

  constructor(private examService: ExamService) {
  }

  ngOnInit() {
    this.examService.getExams()
      .subscribe(exams => {
        this.exams = exams;
        this.selectedExamTitle = exams[1].course.title;
        this.selectedType = exams[0].type;
        this.types = [];
        for (let i = 0; i < exams.length; i++) {
          this.types.push(exams[i].type);
        }
        this.types = Array.from(new Set(this.types));
        this.populateExamsByType();
      });
  }

  onSelectType(type: string) {
    this.selectedType = type;
    this.populateExamsByType();
  }

  onSelectExamTitle(title: string) {
    this.selectedExamTitle = title;
    for (let i = 0; i < this.examsByType.length; i++) {
      if (this.examsByType[i].course.title === title) {
        this.selectedExam = this.examsByType[i];
        break;
      }
    }
  }

  private populateExamsByType() {
    this.examsByType = [];
    for (let i = 0; i < this.exams.length; i++) {
      if (this.exams[i].type === this.selectedType) {
        this.examsByType.push(this.exams[i]);
      }
    }
  }
}
