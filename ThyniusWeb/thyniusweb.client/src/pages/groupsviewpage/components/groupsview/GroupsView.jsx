import React from 'react'
import "./GroupsView.css"
import GroupsViewHeader from './components/groupsviewheader/GroupsViewHeader'
import GroupsViewContainer from './components/groupsviewcontainer/GroupsViewContainer'
import AssureLoggedIn from '../../../../shared/hooks/useAssureLoggedIn'

const GroupsView = () => {
  AssureLoggedIn();
  return (
    <div className='GroupsView'>
        <GroupsViewHeader></GroupsViewHeader>
        <GroupsViewContainer></GroupsViewContainer>
    </div>
  )
}

export default GroupsView