import React from 'react'
import GroupSidebarNavigationItem from './GroupSidebarNavigationItem.jsx/GroupSidebarNavigationItem'
import "./GroupSidebarNavigation.css"

const GroupSidebarNavigation = (props) => {
  return (
    <div className='GroupSidebarNavigation'>
        <GroupSidebarNavigationItem Text="Översikt" link={`/dashboard/group/${props.groupId}/Home`}></GroupSidebarNavigationItem>
        <GroupSidebarNavigationItem Text="Uppgifter" link={`/dashboard/group/${props.groupId}/Assignments`}></GroupSidebarNavigationItem>
    </div>
  )
}

export default GroupSidebarNavigation