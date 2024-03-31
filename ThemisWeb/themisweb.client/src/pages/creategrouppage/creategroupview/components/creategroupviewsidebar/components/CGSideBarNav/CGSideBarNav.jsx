import React from 'react'
import "../../../../../../../shared/styles/DashboardContainer.css"
import { Link } from 'react-router-dom'

export const CGSideBarNav = () => {
  return (
    <div className='DashboardContainer-SideBar-Navigation'>
        <div className='DashboardContainer-SideBar-Navigation-Title'>Inställningar</div>
        <div className='DashboardContainer-SideBar-Navigation-Section'>
             <Link className="DashboardContainer-SideBar-Navigation-Item" to="/dashboard/creategroup/general">Allmänt</Link>
             <Link className="DashboardContainer-SideBar-Navigation-Item" to="/dashboard/creategroup/members">Medlemmar</Link>
        </div>
    </div>
  )
}