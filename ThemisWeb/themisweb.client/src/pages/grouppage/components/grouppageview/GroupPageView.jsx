import React from 'react'
import "./GroupPageView.css"
import { useParams } from 'react-router-dom'
import useExtendedGroupInfo from './hooks/useExtendedGroupInfo'
import GroupMainContent from './components/groupmaincontent/GroupMainContent'
import GroupSideBar from './components/groupsidebar/GroupSideBar'

const GroupPageView = () => {
  let { id, page } = useParams();
  const groupInfo = useExtendedGroupInfo(id);
  return (
    <div className='GroupPageView'>
      <div className='GroupContainer'>
        <GroupSideBar groupId={id} GroupName={groupInfo?.Name}></GroupSideBar> 
        <GroupMainContent groupInfo={groupInfo} Page={page}></GroupMainContent> 
      </div>
    </div>
  )
}// some prop drilling but cba useContext for a depth of 2

export default GroupPageView