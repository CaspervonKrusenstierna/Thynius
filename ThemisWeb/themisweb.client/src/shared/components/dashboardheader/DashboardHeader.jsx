import React from 'react'
import "./DashboardHeader.css"
import DashboardNavItem from './components/DashboardNavItem/DashboardNavItem'
import DashboardUserComponent from './components/DashboardUserComponent/DashboardUserComponent'
const DashboardHeader = () => {
  return (
    <div className='DashboardHeader'>
      <div className='DashboardHeader-Left'>
        <DashboardNavItem title="Home" link="/dashboard/home"></DashboardNavItem>
        <DashboardNavItem title="Groups" link="/dashboard/groups"></DashboardNavItem>
        <DashboardNavItem title="Calendar" link="/dashboard/calendar"></DashboardNavItem>
      </div>
      <div className='DashboardHeader-Middle'>M</div>
      <div className='DashboardHeader-Right'><DashboardUserComponent></DashboardUserComponent></div>
    </div>
  )
}

export default DashboardHeader