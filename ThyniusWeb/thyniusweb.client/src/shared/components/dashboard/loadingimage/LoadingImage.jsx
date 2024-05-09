import { Spinner } from '@nextui-org/spinner';
import React, { useState } from 'react'

const LoadingImage = (props) => {
  const [imageIsLoading, setImageIsLoading] = useState(true);
  return (
    <>
    {imageIsLoading ? <div className={props.className + " flex items-center justify-center"}><Spinner/></div>
    : <></>            }
    <img onLoad={() => {setImageIsLoading(false)}} className={imageIsLoading ? props.className + " absolute opacity-0" : props.className} src={props.img}></img>
    </>
  )
}

export default LoadingImage