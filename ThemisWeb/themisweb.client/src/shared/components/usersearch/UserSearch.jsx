import React, { useEffect, useRef, useState } from 'react'
import "./UserSearch.css"
import { MagnifyingClass } from '../../assets';
import SearchSuggestionContainer from './searchsuggestioncontainer/SearchSuggestionContainer';
import AddUserButton from './searchsuggestioncontainer/components/adduserbutton/AddUserButton';

const UserSearch = (props) => {
  const [showInput, setShowInput] = useState(true);
  const [textInput, setTextInput] = useState("");
  const [forceReactRerender, setForceReactRerender] = useState(false);
  const isManipulated = useRef(true); // used to check if the user that is being added is a real user.
  const currUser = useRef();
  return (
    <div onClick={() => {setShowInput(true)}} className='UserSearch'>
        {showInput ? 
        <div className='ShowInputSearchBar'><input id="SearchInput" value={textInput} autoFocus={true} onChange={(s) => {isManipulated.current = true; setTextInput(s.target.value)}} className='UserSearch-Input'></input>
            <AddUserButton onClick={() => {if(!isManipulated.current){props.onSubmit(currUser.current)}}}></AddUserButton>
        </div>
        : 
        <div className='SearchBar'>
            <img className="UserSearch-Img" src={MagnifyingClass}></img>
            <p className='UserSearch-Text'>Sök</p>
        </div>
        }

        {(textInput != "" ) ? <SearchSuggestionContainer onChoose={(s) => {isManipulated.current = false; currUser.current = s; setTextInput(s.username); setForceReactRerender(!forceReactRerender)}} search={textInput} disable={!isManipulated.current}></SearchSuggestionContainer> : <></>}
    </div>
  )
}// the t and slice stuff is used to force react to rerender 

export default UserSearch