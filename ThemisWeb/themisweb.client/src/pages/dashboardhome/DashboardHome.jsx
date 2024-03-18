import React from 'react'
import "../../shared/styles/Page.css"
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'

const DashboardHome = () => {
  return (
  <div className='Page-Container'>
    <DashboardHeader></DashboardHeader>
    <div className='Page'>
    </div>
  </div>
  )
}

export default DashboardHome