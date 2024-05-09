import { Skeleton } from '@nextui-org/skeleton'
import React from 'react'
import { Link } from 'react-router-dom'
//these styles are from shared/DashboardContainer

const DashboardContainerSideBar = (props) => {
  return (
    <div className='DashboardContainer-SideBar'>
        <div className='DashboardContainer-SideBar-Header'>
          <Skeleton className='DashboardContainer-SideBar-Header-Title rounded before:!duration-1000' isLoaded={props.sidebartitle}>{props.sidebartitle ? props.sidebartitle : "lalalalala"}</Skeleton>
        </div>
        <div className='DashboardContainer-SideBar-Navigation'>
            <Skeleton isLoaded={props.sidebartitle} className='DashboardContainer-SideBar-Navigation-Title rounded before:!duration-1000'>{props.navigationtitle}</Skeleton>
            <div className='DashboardContainer-SideBar-Navigation-Section'>
                {props.tabs?.map((tab, index) => props.sidebartitle ? <Link key={index} state={props.state} className='DashboardContainer-SideBar-Navigation-Item' to={tab.link}>{tab.name}</Link> : <Skeleton className='DashboardContainer-SideBar-Navigation-Item rounded before:!duration-1000'>{tab.name}</Skeleton>)}
            </div>
        </div>
    </div>

  )
}

export default DashboardContainerSideBar