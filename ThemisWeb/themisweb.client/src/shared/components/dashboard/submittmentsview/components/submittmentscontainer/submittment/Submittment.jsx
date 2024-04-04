import React, { useMemo } from 'react'
import { Link } from 'react-router-dom'

//styles from shared/styles/submittmentsview
const Submittment = (props) => {

  const warningMessage = useMemo(() => {
    switch(props.warningLevel){
      case 0: return <div className="text-green-600 mr-7 text-sm sm:text-base">Inga tecken på fusk</div>
      case 1: return <div className="text-yellow-400 mr-7 text-sm sm:text-base">Tecken på fusk</div>
      case 2: return <div className="text-red-500 mr-7 text-sm sm:text-base">Stora tecken på fusk</div>
      default: return <div className='text-red-500 mr-7 text-sm sm:text-base'>ERROR NO DATA</div>
    }
  }, [props.warningLevel])

  return (
    <Link to={"/dashboard/submittment/" + props.id} className='Submittment'>
        <p className='text-sm sm:text-base lg:text-large'>{props.username}</p>
        {warningMessage}
    </Link>
  )
}

export default Submittment