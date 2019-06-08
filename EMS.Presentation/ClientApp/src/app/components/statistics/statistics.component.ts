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
  types: string[];
  exams: Exam[];

  constructor(private examService: ExamService) {
  }

  ngOnInit() {
    this.examService.getExams()
      .subscribe(exams => {
        this.exams = Array.from(new Set(exams));
        this.selectedExamTitle = exams[0].course.title;
        this.selectedType = exams[0].type;
        this.types = [];
        for (let i = 0; i < exams.length; i++) {
          this.types.push(exams[i].type);
        }
        this.types = Array.from(new Set(this.types));
      });

  }

  onSelectType(type: string) {
    this.selectedType = type;
  }

  onSelectExamTitle(title: string) {
    this.selectedExamTitle = title;
  }
}
