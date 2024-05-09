import React, { useContext } from 'react'
import "./DashboardHeader.css"
import DashboardUserComponent from './components/DashboardUserComponent/DashboardUserComponent'
import getDashboardHeaderNavItems from './getDashboardHeaderNavItems'
import { sessionInfoContext } from '../../../../App'

const DashboardHeader = () => {
  const SessionInfo = useContext(sessionInfoContext)?.sessionInfo;
  let DashboardHeaderNavItems = getDashboardHeaderNavItems(SessionInfo?.RoleLevel);

  return (
    <div className='DashboardHeader'>
      <div className='DashboardHeader-Left'>
        {DashboardHeaderNavItems}
      </div>
      <div className='DashboardHeader-Middle'></div>
      <div className='DashboardHeader-Right'><DashboardUserComponent></DashboardUserComponent></div>
    </div>
  )
}

export default DashboardHeader