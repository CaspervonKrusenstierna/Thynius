import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import "../../shared/styles/Page.css"
import CalendarPageView from './components/calendarpageview/CalendarPageView'

const CalendarPage = () => {
  return (
    <div className='Page-Container'>
        <DashboardHeader></DashboardHeader>
        <CalendarPageView></CalendarPageView>
    </div>
  )
}

export default CalendarPage