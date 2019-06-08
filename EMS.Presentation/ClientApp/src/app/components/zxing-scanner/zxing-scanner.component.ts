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
    const studentId = localStorage.getItem('userID');
    const examId = this.route.snapshot.paramMap.get('id');
    let url = $event.split('/');
    url[6] = studentId;
    $event = url.join('/');

    this.studentService.checkIn(studentId, examId, $event)
      .subscribe(() => this.location.back(),
        err => {
          alert('Cod QR invalid');
        });
  }
}
