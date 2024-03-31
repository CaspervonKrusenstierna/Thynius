import React, { createContext, useMemo } from 'react'
import { useParams } from 'react-router-dom'
import useExtendedGroupInfo from './hooks/useExtendedGroupInfo'
import "../../../shared/styles/DashboardContainer.css"
import ResolveGroupContent from './hooks/ResolveGroupContent'
import DashboardContainerSideBar from '../../../shared/components/dashboardcontainersidebar/DashboardContainerSideBar'
import useGroupTabs from './hooks/useGroupTabs'

export const groupinfoContext = createContext();

const GroupPageView = () => {
  let { id } = useParams();
  const groupInfo = useExtendedGroupInfo(id);
  const pageContent = ResolveGroupContent();
  const GroupTabs = useGroupTabs(id);

  return (
    <groupinfoContext.Provider value={groupInfo}>
      <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
          <DashboardContainerSideBar sidebartitle={groupInfo?.Name} navigationtitle="Information" tabs={GroupTabs}></DashboardContainerSideBar>
          <div className='DashboardContainer-Content'>
            {pageContent}
          </div>
        </div>
      </div>
    </groupinfoContext.Provider>
  )
}

export default GroupPageView