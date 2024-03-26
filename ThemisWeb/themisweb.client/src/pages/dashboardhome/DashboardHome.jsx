import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import DashboardHomeView from './dashboardhomeview/DashboardHomeView'

const DashboardHome = () => {
  return (
  <>
    <DashboardHeader></DashboardHeader>
    <DashboardHomeView></DashboardHomeView>
  </>
  )
}

export default DashboardHome