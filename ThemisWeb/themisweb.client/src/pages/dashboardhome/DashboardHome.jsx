import React from 'react'
import "../../shared/styles/Page.css"
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import DashboardHomeView from './dashboardhomeview/DashboardHomeView'

const DashboardHome = () => {
  return (
  <div className='Page-Container'>
    <DashboardHeader></DashboardHeader>
    <DashboardHomeView></DashboardHomeView>
  </div>
  )
}

export default DashboardHome