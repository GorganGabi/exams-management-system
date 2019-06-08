import {Component, OnInit} from '@angular/core';
import {StudentService} from '../../services/student.service';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-zxing-scanner',
  templateUrl: './zxing-scanner.component.html',
  styleUrls: ['./zxing-scanner.component.css']
})
export class ZxingScannerComponent implements OnInit {

  constructor(private studentService: StudentService,
              private route: ActivatedRoute,
              private location: Location) {
  }

  ngOnInit() {
  }

  scanSuccessHandler($event: string) {
    console.log($event);
    console.log(typeof $event);

    const studentId = localStorage.getItem('userID');
    const examId = this.route.snapshot.paramMap.get('id');
    $event = `http://localhost:11111/api/v1/students/${studentId}/exams/${examId}`;

    this.studentService.checkIn(studentId, examId, $event)
      .subscribe(() => this.location.back());
  }
}
