import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';

@Component({
  selector: 'app-gauss',
  templateUrl: './gauss.component.html',
  styleUrls: ['./gauss.component.css']
})
export class GaussComponent implements OnInit {
  public lineChartData: ChartDataSets[] = [
    { data: [5, 10, 15, 20, 15, 10], label: 'Toate notele' },
  ];
  public lineChartLabels: Label[] = ['10', '9', '8', '7', '6', '5'];
  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(51, 102, 204)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor() { }

  ngOnInit() {
  }

}
