import React from 'react'
import { Link } from 'react-router-dom'
//these styles are from shared/DashboardContainer

const DashboardContainerSideBar = (props) => {
  return (
    <div className='DashboardContainer-SideBar'>
        <div className='DashboardContainer-SideBar-Header'>
            <p className='DashboardContainer-SideBar-Header-Title'>{props.sidebartitle}</p>
        </div>
        <div className='DashboardContainer-SideBar-Navigation'>
            <p className='DashboardContainer-SideBar-Navigation-Title'>{props.navigationtitle}</p>
            <div className='DashboardContainer-SideBar-Navigation-Section'>
                {props.tabs?.map(tab => <Link className='DashboardContainer-SideBar-Navigation-Item' to={tab.link}>{tab.name}</Link>)}
            </div>
        </div>
    </div>

  )
}

export default DashboardContainerSideBar