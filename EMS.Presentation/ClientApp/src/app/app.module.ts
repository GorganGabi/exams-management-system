import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './course-management/courses/courses.component'
import { ExamsComponent } from './exam-management/exams/exams.component';
import { CourseDetailComponent } from './course-management/course-detail/course-detail.component';
import { LoginComponent } from './login/login.component';
import { AddExamComponent } from './exam-management/add-exam/add-exam.component';
import { ExamDetailsComponent } from './exam-management/exam-details/exam-details.component';
import { AddCourseComponent } from './course-management/add-course/add-course.component';

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
    AddCourseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'courses', component: CoursesComponent },
      { path: 'courses/create', component: AddCourseComponent },
      { path: 'courses/:id', component: CourseDetailComponent },
      { path: 'exams', component: ExamsComponent },
      { path: 'exams/create', component: AddExamComponent },
      { path: 'exams/:id', component: ExamDetailsComponent },
      { path: 'login', component: LoginComponent}
    ])
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
