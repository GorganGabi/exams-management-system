import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {Student} from '../../../models/student';
import {StudentService} from '../../../services/student.service';
import {debounceTime, distinctUntilChanged, switchMap} from 'rxjs/operators';
import {ActivatedRoute} from '@angular/router';
import {ExamService} from '../../../services/exam.service';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {
  students$: Observable<Student[]>;
  private searchTerms = new Subject<string>();
  name = '';
  courseName: string;
  @Output() OnStudentName: EventEmitter<any> = new EventEmitter<any>();

  constructor(private studentService: StudentService,
              private route: ActivatedRoute,
              private examService: ExamService) {
  }

  search(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnInit() {
    this.examService.getExam(this.route.snapshot.paramMap.get('id'))
      .subscribe(exam => {
        this.courseName = exam.course.title;
      });

    this.students$ = this.searchTerms.pipe(
      debounceTime(300),

      distinctUntilChanged(),

      switchMap((term: string) => this.studentService.getStudentsByNameAndCourse(term, this.courseName))
    );
  }

  public setInputValue(name: string): void {
    this.name = name;
    this.OnStudentName.emit(name);
    this.students$ = null;
  }
}
