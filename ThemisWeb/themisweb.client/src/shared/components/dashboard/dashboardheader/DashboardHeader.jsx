import React, { useContext } from 'react'
import "./DashboardHeader.css"
import DashboardNavItem from './components/DashboardNavItem/DashboardNavItem'
import DashboardUserComponent from './components/DashboardUserComponent/DashboardUserComponent'
import { Home } from 'lucide-react'

const DashboardHeader = () => {
  return (
    <div className='DashboardHeader'>
      <div className='DashboardHeader-Left'>
        <DashboardNavItem title="Home" link="/dashboard/home" item={<Home></Home>}></DashboardNavItem>
        <DashboardNavItem title="Grupper" link="/dashboard/groups"></DashboardNavItem>
        <DashboardNavItem title="Texter" link="/dashboard/texts"></DashboardNavItem>
      </div>
      <div className='DashboardHeader-Middle'>M</div>
      <div className='DashboardHeader-Right'><DashboardUserComponent></DashboardUserComponent></div>
    </div>
  )
}

export default DashboardHeader