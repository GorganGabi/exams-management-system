import {Component, Input, OnInit} from '@angular/core';
import {ChartDataSets, ChartOptions, ChartType} from 'chart.js';
import {Color, Label} from 'ng2-charts';
import {Exam} from "../../../models/exam";

@Component({
  selector: 'app-gauss',
  templateUrl: './gauss.component.html',
  styleUrls: ['./gauss.component.css']
})
export class GaussComponent implements OnInit {
  @Input() selectedExam: Exam;

  noOfGrades: number[];
  public lineChartData: ChartDataSets[];
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

  constructor() {
  }

  ngOnInit() {
    this.noOfGrades = [5, 10, 15, 20, 15, 10];
    this.lineChartData = [
      {data: this.noOfGrades, label: 'Toate notele'},
    ];
  }

}
