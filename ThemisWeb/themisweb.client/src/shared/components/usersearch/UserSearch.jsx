import React, { useEffect, useRef, useState } from 'react'
import "./UserSearch.css"
import { MagnifyingClass } from '../../assets';
import SearchSuggestionContainer from './searchsuggestioncontainer/SearchSuggestionContainer';
import AddUserButton from './searchsuggestioncontainer/components/adduserbutton/AddUserButton';
import useForceReactRerender from '../../hooks/useForceReactRerender';
import useSearchSuggestions from './useSearchSuggestions';

const UserSearch = (props) => {
  const [showInput, setShowInput] = useState(true);
  const [textInput, setTextInput] = useState("");
  const [searchSuggestions, setSearchSuggestions] = useSearchSuggestions(textInput);

  const currUser = useRef();

  function onSearchSuggestionSelect(chosenSearchSuggestion){
    currUser.current = chosenSearchSuggestion;
    setTextInput(chosenSearchSuggestion.username);
    useForceReactRerender();
  }

  function onAddUser(){
    if(currUser.current){
      props.onSubmit(currUser.current);
      setSearchSuggestions([]);
    }
  }

  function onInputChange(newValue){
    currUser.current = null;
    setTextInput(s.target.value);
  }

  return (
    <div onClick={() => {setShowInput(true)}} className='UserSearch'>
        {showInput ? 
        <div className='ShowInputSearchBar'><input id="SearchInput" value={textInput} autoFocus={true} onChange={onInputChange} className='UserSearch-Input'></input>
            <AddUserButton onClick={onAddUser}></AddUserButton>
        </div>
        : 
        <div className='SearchBar'>
            <img className="UserSearch-Img" src={MagnifyingClass}></img>
            <p className='UserSearch-Text'>Sök</p>
        </div>
        }

        {(textInput != "" ) ? <SearchSuggestionContainer searchSuggestions={searchSuggestions}></SearchSuggestionContainer> : <></>}
    </div>
  )
}// the t and slice stuff is used to force react to rerender 

export default UserSearch