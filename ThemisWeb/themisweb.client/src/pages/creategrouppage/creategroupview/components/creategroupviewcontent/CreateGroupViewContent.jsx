import React, { useCallback, useRef, useState } from 'react'
import "./CreateGroupViewContent.css"
import VerticalInput from '../../../../../shared/components/verticalinput/VerticalInput'
import { ErrorMessage, SubmitButton } from '../../../../../shared/components'
import CreateGroupSubmitButton from './components/creategroupsubmitbutton/CreateGroupSubmitButton'
import useFetch from '../../../../../shared/hooks/useFetch'
import { useNavigate } from 'react-router-dom'

const CreateGroupViewContent = () => {
    const groupName = useRef("");
    const [errorMessage, setErrorMessage] = useState();
    const navigator = useNavigate();
    const cachedOnSubmitClick = useCallback(async () => {
        const response = await useFetch("/group?GroupName=" + groupName.current, "POST");
        if(response.ok){
            navigator("/dashboard/groups");
        }
        response.json().then(s => {setErrorMessage(s.Title)});
    }, [groupName.Val])

  return (
    <div className='CreateGroupViewContent'>
        <div className='CreateGroupViewContent-TopContainer'>
            {errorMessage ? <ErrorMessage message={errorMessage}></ErrorMessage> : <></>}
            <VerticalInput onChange={(e) => {groupName.current=e.target.value}}Title="Namn"></VerticalInput>
        </div>
        <div className='CreateGroupViewContent-BottomContainer'>
            <CreateGroupSubmitButton onClick={cachedOnSubmitClick}></CreateGroupSubmitButton>
        </div>
    </div>
  )
}

export default CreateGroupViewContent