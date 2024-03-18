import React from 'react'
import Particles from "react-tsparticles";
import { loadSlim } from "tsparticles-slim";
import { useCallback } from 'react'
import "./WaveBackgroundBox.css"
import Wave from './wave.svg';

const WaveBackgroundBox = () => {
  const particlesInit = useCallback(async engine => {
        await loadSlim(engine);
    }, []);
  return (
    <>
     <div className='WaveContainerBox'>
        <img src={Wave}></img>
    </div>
     <div className='WaveBackgroundBox'>
              <Particles
            id="tsparticles"
            init={particlesInit}
            options={{
                fpsLimit: 120,
                particles: {
                    color: {
                        value: "#ffffff",
                    },
                    links: {
                        color: "#ffffff",
                        distance: 150,
                        enable: true,
                        opacity: 0.5,
                        width: 1,
                    },
                    move: {
                        direction: "none",
                        enable: true,
                        outModes: {
                            default: "bounce",
                        },
                        random: false,
                        speed: 3,
                        straight: false,
                    },
                    number: {
                        density: {
                            enable: true,
                            area: 800,
                        },
                        value: 80,
                    },
                    opacity: {
                        value: 0.5,
                    },
                    shape: {
                        type: "circle",
                    },
                    size: {
                        value: { min: 1, max: 5 },
                    },
                },
                detectRetina: true,
            }}
        />
     </div>
    </>
  )
}

export default WaveBackgroundBox