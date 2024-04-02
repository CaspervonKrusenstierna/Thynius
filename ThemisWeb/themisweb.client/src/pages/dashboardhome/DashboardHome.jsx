import React from 'react'
import DashboardHomeView from './dashboardhomeview/DashboardHomeView'
import { DashboardHeader } from '../../shared/components/dashboard'

const DashboardHome = () => {
  return (
  <>
    <DashboardHeader></DashboardHeader>
    <DashboardHomeView></DashboardHomeView>
  </>
  )
}

export default DashboardHome