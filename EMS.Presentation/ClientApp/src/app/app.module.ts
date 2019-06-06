import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {CoursesComponent} from './components/course-management/courses/courses.component'
import {ExamsComponent} from './components/exam-management/exams/exams.component';
import {CourseDetailComponent} from './components/course-management/course-detail/course-detail.component';
import {LoginComponent} from './components/login/login.component';
import {AddExamComponent} from './components/exam-management/add-exam/add-exam.component';
import {ExamDetailsComponent} from './components/exam-management/exam-details/exam-details.component';
import {AddCourseComponent} from './components/course-management/add-course/add-course.component';
import {GradesComponent} from './components/grade-management/grades/grades.component';
import {CourseGradesComponent} from './components/course-management/course-grades/course-grades.component';
import {AddGradeComponent} from './components/grade-management/add-grade/add-grade.component';
import {Course} from './models/course';
import {ExamGradesComponent} from './components/exam-management/exam-grades/exam-grades.component';
import {GradeUpdateComponent} from './components/grade-management/grade-update/grade-update.component';
import {StatisticsComponent} from './components/statistics/statistics.component';
import {StudentSearchComponent} from './components/student-management/student-search/student-search.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CoursesComponent,
    CourseDetailComponent,
    ExamsComponent,
    LoginComponent,
    AddExamComponent,
    ExamDetailsComponent,
    AddCourseComponent,
    GradesComponent,
    CourseGradesComponent,
    CourseDetailComponent,
    AddGradeComponent,
    ExamGradesComponent,
    GradeUpdateComponent,
    StatisticsComponent,
    StudentSearchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'courses', component: CoursesComponent},
      {path: 'courses/create', component: AddCourseComponent},
      {path: 'courses/:id', component: CourseDetailComponent},
      {path: 'courses/:id/grades', component: CourseGradesComponent, data: {course: Course}},
      {path: 'exams', component: ExamsComponent},
      {path: 'exams/create', component: AddExamComponent},
      {path: 'exams/grades', component: ExamGradesComponent},
      {path: 'exams/:id', component: ExamDetailsComponent},
      {path: 'exams/:id/grades', component: ExamGradesComponent},
      {path: 'exams/:id/grades/create', component: AddGradeComponent},
      {path: 'grades', component: GradesComponent},
      {path: 'login', component: LoginComponent},
      {path: 'statistics', component: StatisticsComponent}
    ])
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
