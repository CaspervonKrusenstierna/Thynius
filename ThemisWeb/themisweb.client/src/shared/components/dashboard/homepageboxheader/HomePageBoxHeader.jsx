import React from 'react'
import X from "lucide-react"
import {useNavigate} from "react-router-dom"

const HomePageBoxHeader = () => {
    const navigate = useNavigate();
  return (
    <div className='ExitButtonContainer'>
        <button className='h-10 w-10' onClick={() => {navigate("/")}}><X className='w-full h-full'></X></button>
    </div>
  )
}

export default HomePageBoxHeader