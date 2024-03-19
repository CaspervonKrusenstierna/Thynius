import React from 'react'
import "./CreateGroupView.css"
import CreateGroupViewHeader from './components/creategroupviewheader/CreateGroupViewHeader'
import CreateGroupViewContent from './components/creategroupviewcontent/CreateGroupViewContent'

const CreateGroupView = () => {
  return (
    <div className='CreateGroupView'>
        <CreateGroupViewHeader></CreateGroupViewHeader>
        <CreateGroupViewContent></CreateGroupViewContent>
    </div>
  )
}

export default CreateGroupView