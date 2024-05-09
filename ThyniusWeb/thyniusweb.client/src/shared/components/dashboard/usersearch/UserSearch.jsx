import React, { useEffect, useRef, useState } from 'react'
import "./UserSearch.css"
import SearchSuggestionContainer from './searchsuggestioncontainer/SearchSuggestionContainer';
import useSearchSuggestions from './useSearchSuggestions';
import { Button } from "@/components/ui/button"
import { MagnifyingClass } from '../../../assets';
import { useTranslation } from 'react-i18next';

const UserSearch = (props) => {
  const [t, i18n] = useTranslation();
  const [showInput, setShowInput] = useState(false);
  const [textInput, setTextInput] = useState("");
  const [searchSuggestions, setSearchSuggestions] = useSearchSuggestions(textInput);
  const [disableSearchSuggestions, setDisableSearchSuggestions] = useState(false);

  const currUser = useRef();

  function onSearchSuggestionSelect(chosenSearchSuggestion){
    currUser.current = chosenSearchSuggestion;
    setTextInput(chosenSearchSuggestion.username);
    setDisableSearchSuggestions(true);
  }

  function onInputBlur(e){
    if(textInput == ""){
      setShowInput(false);
    }
    else if(e.relatedTarget?.className != "SearchSuggestion"){
      setSearchSuggestions([]);
    }
  }

  function onAddUser(){
    if(props.nonStrictMode){
      props.onSubmit(textInput)
      return;
    }
    if(currUser.current){
      props.onSubmit(currUser.current);
      setSearchSuggestions([]);
    }
  }

  function onInputChange(newValue){
    currUser.current = null;
    setDisableSearchSuggestions(false);
    setTextInput(newValue);
  }

  return (
    <div onClick={() => {setShowInput(true)}} className='UserSearch'>
        {showInput ? 
        <div className='SearchBarContainer'><input id="SearchInput" autoComplete="off" onBlur={onInputBlur} value={textInput} autoFocus={true} onChange={(s) => {onInputChange(s.target.value)}} className='UserSearch-Input'></input>
            <Button onClick={onAddUser} className="UserSearchAddButton text-black"><p>{t("Add User")}</p></Button>
        </div>
        : <div className='SearchBarContainer'>
        <div className='SearchBar'>
            <img className="UserSearch-Img" src={MagnifyingClass}></img>
            <p className='UserSearch-Text'>{t("Search")}</p>
        </div>
        <Button onClick={onAddUser} className="text-black hover:text-black">{t("Add User")}</Button></div>
        }
        {!disableSearchSuggestions ? <SearchSuggestionContainer textInput={textInput} onChoose={onSearchSuggestionSelect} searchSuggestions={searchSuggestions}></SearchSuggestionContainer> : <></>}
    </div>
  )
}

export default UserSearch