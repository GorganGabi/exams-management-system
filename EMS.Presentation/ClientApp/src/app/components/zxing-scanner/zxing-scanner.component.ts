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
    // console.log($event);
    this.studentService.checkIn(localStorage.getItem('userID'), this.route.snapshot.paramMap.get('id'))
      .subscribe(() => this.location.back());
  }
}
