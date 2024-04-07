import React from 'react'
import "./GroupsView.css"
import GroupsViewHeader from './components/groupsviewheader/GroupsViewHeader'
import GroupsViewContainer from './components/groupsviewcontainer/GroupsViewContainer'
import {RemoveScroll} from 'react-remove-scroll';
const GroupsView = () => {
  return (
    <div className='GroupsView'>
        <GroupsViewHeader></GroupsViewHeader>
        <GroupsViewContainer></GroupsViewContainer>
    </div>
  )
}

export default GroupsView