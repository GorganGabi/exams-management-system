import {Component, Input, OnInit} from '@angular/core';
import {ChartDataSets, ChartOptions, ChartType} from 'chart.js';
import {Color, Label} from 'ng2-charts';
import {Exam} from '../../../models/exam';
import {ExamService} from '../../../services/exam.service';

@Component({
  selector: 'app-gauss',
  templateUrl: './gauss.component.html',
  styleUrls: ['./gauss.component.css']
})
export class GaussComponent implements OnInit {
  private _exam: Exam;
  noOfGrades: number;

  @Input()
  set selectedExam(selectedExam: Exam) {
    this._exam = selectedExam;
    if (selectedExam !== undefined) {
      this.populateChart();
    }
  }

  public lineChartData: ChartDataSets[] = [
    {data: [0, 0, 0, 0, 0, 0], label: 'Toate notele'},
  ];
  public lineChartLabels: Label[] = ['10', '9', '8', '7', '6', '5'];
  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(23, 100, 321)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor(private examService: ExamService) {
  }

  ngOnInit() {

  }

  private populateChart() {
    this.examService.getExamGrades(this._exam.id)
      .subscribe(grades => {
        this.noOfGrades = grades.length;
        // todo: don't hardcode
        const grade10 = this.noOfGrades * 0.05;
        const grade9 = grade10 * 0.1;
        const grade8 = grade9 * 0.15;
        const grade7 = grade8 * 0.25;
        const grade6 = grade7 * 0.15;
        const grade5 = grade6 * 0.10;
        this.lineChartData =  [
          {data: [grade10, grade9, grade8, grade7, grade6, grade5], label: 'Toate notele'},
        ];
      });
  }
}
