import {Component, Input, OnInit, SimpleChanges} from '@angular/core';
import {ChartDataSets, ChartOptions, ChartType} from 'chart.js';
import {Color, Label} from 'ng2-charts';
import {ExamService} from '../../../services/exam.service';
import {Exam} from '../../../models/exam';

@Component({
  selector: 'app-grade-statistics',
  templateUrl: './grade-statistics.component.html',
  styleUrls: ['./grade-statistics.component.css']
})
export class GradeStatisticsComponent implements OnInit {
  private _exam: Exam;

  @Input()
  set selectedExam(selectedExam: Exam) {
    this._exam = selectedExam;
    if (selectedExam !== undefined) {
      this.setGrades();
    }
  }


  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = ['10', '9', '8', '7', '6', '5', '4', '3', '2', '1'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];
  public barChartColors: Color[] = [
    {
      backgroundColor: 'rgba(51, 102, 204)'
    },
    {
      backgroundColor: 'rgba(100, 150, 200)'
    }
  ];

  public barChartData: ChartDataSets[] = [
    {data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], label: 'Semianul A'},
    {data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], label: 'Semianul B'}
  ];

  constructor(private examService: ExamService) {
  }

  ngOnInit() {

  }

  setGrades() {
    this.examService.getExamGrades(this._exam.id)
      .subscribe(grades => {
        const gradesA = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        const gradesB = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        // todo: create specialty dynamically
        // todo: consider refactoring
        for (let i = 0; i < grades.length; i++) {
          if (grades[i].student.specialty === 'Semian A') {
            gradesA[grades[i].value]++;
          } else {
            gradesB[grades[i].value]++;
          }
        }

        this.barChartData = [
          {data: gradesA.reverse(), label: 'Semian A'},
          {data: gradesB.reverse(), label: 'Semian B'}
        ];
      });
  }
}
