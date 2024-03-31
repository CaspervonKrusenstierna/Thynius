import React from 'react'
import "./GroupImageSelection.css"
import GroupImageSelfSelect from './components/GroupImageSelfSelect/GroupImageSelfSelect'
import { GroupImageSuggestions } from './components/GroupImageSuggestions/GroupImageSuggestions'

const GroupImageSelection = () => {
  return (
    <div className='GroupImageSelection'>
        <p className='GroupImageSelfSelect-ContainerTitle'>Välj bild</p>
        <GroupImageSelfSelect></GroupImageSelfSelect>
    </div>
  )
}

export default GroupImageSelection