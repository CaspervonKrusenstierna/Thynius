import React from 'react'
import "./CreateGroupMainPage.css"
import { GroupNameInput } from './components/GroupNameInput/GroupNameInput'
import GroupImageSelection from './components/GroupImageSelection/GroupImageSelection'

const CreateGroupMainPage = (props) => {
  return (
    <div className='CreateGroupMainPage'>
        <div className='CGMP-LeftContainer'>
            <GroupNameInput></GroupNameInput>
        </div>
        <div className='CGMP-RightContainer'>
            <GroupImageSelection></GroupImageSelection>
        </div>
    </div>
  )
}

export default CreateGroupMainPage