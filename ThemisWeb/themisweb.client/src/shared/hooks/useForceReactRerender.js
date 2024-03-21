import { useState } from "react";

export default function useForceReactRerender() {
    const [temp, setTemp] = useState();
    setTemp("Rerender!");
}
