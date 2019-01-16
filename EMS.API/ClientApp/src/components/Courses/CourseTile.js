import React      from 'react';

import '../../styles/css/CourseTile.css'

class CourseTile extends React.Component {
  render() {
    return (
      <a href={`/courses/${this.props.course.id}`} className='course-tile panel panel-sm'>
        <div className="course-tile__title">{this.props.course.title}</div>

        <div className="course-tile__meta">
          <div className="course-tile__meta--universityYear">An Universitar: {this.props.course.universityYear}</div>
          <div className="course-tile__meta--studentYear">Anul {this.props.course.studentYear}</div>
          <div className="course-tile__meta--semester">Semestrul {this.props.course.semester}</div>
        </div>
      </a>
    )
  }
}

export default CourseTile;
