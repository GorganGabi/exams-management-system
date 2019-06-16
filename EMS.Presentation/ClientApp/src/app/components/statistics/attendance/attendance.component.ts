import {Component, Input, OnInit} from '@angular/core';
import {ChartDataSets, ChartOptions, ChartType} from 'chart.js';
import {Label} from 'ng2-charts';
import {Exam} from '../../../models/exam';
import {StudentService} from '../../../services/student.service';
import {ExamService} from '../../../services/exam.service';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {
  private _exam: Exam;

  @Input()
  set selectedExam(selectedExam: Exam) {
    this._exam = selectedExam;
    if (selectedExam !== undefined) {
      this.populateChart(selectedExam);
    }
  }

  get selectedExam() {
    return this._exam;
  }

  noOfStudents: number;
  noOfGrades: number;

  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = ['Prezenti', 'Absenti'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [
    {data: [0, 0], label: 'Semian A'},
    {data: [0, 0], label: 'Semian B'}
  ];

  constructor(private examService: ExamService,
              private studentService: StudentService) {
  }

  ngOnInit() {
    this.studentService.getStudents()
      .subscribe(students => this.noOfStudents = students.length);
  }

  private populateChart(exam: Exam) {
    this.examService.getExamGrades(exam.id)
      .subscribe(grades => {
        this.noOfGrades = grades.length;
        let noOfGradesA = 0, noOfGradesB = 0;
        // todo: create specialty dynamically
        for (let i = 0; i < grades.length; i++) {
          if (grades[i].student.specialty === 'Semian A') {
            noOfGradesA++;
          } else {
            noOfGradesB++;
          }
        }

        this.barChartData = [
          {data: [noOfGradesA, this.noOfStudents - noOfGradesA], label: 'Semian A'},
          {data: [noOfGradesB, this.noOfStudents - noOfGradesB], label: 'Semian B'}
        ];
      });

  }
}
