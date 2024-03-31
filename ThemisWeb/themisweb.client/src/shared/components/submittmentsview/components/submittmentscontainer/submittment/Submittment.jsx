import React, { useMemo } from 'react'
import "./Submittment.css"
import { Link } from 'react-router-dom'

const Submittment = (props) => {

  const warningMessage = useMemo(() => {
    switch(props.warningLevel){
      case 0: return <div className="text-green-600 mr-7 text-base">Inga tecken på fusk</div>
      case 1: return <div className="text-yellow-400 mr-7 text-base">Tecken på fusk</div>
      case 2: return <div className="text-red-500 mr-7 text-base">Stora tecken på fusk</div>
      default: return <div className='text-red-500 mr-7 text-base'>ERROR NO DATA</div>
    }
  }, [props.warningLevel])

  return (
    <Link to={"/dashboard/submittment/" + props.id} className='Submittment'>
        <p className='Submittment-Text'>{props.username}</p>
        {warningMessage}
    </Link>
  )
}

export default Submittment