import React from 'react'
import "./CreateGroupView.css"
import CreateGroupViewHeader from './components/creategroupviewheader/CreateGroupViewHeader'
import CreateGroupViewContent from './components/creategroupviewcontent/CreateGroupViewContent'

const CreateGroupView = () => {
  return (
    <div className='CreateGroupView'>
      <div className='CreateGroupContainer'>
        <CreateGroupViewHeader></CreateGroupViewHeader>
        <CreateGroupViewContent></CreateGroupViewContent>
      </div>
    </div>
  )
}

export default CreateGroupView