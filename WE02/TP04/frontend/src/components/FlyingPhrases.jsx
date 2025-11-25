import React, { useEffect, useState } from 'react';
import { motion } from 'framer-motion';

export default function FlyingPhrases({ phrases }) {
  const [visible, setVisible] = useState([]);

  useEffect(()=> {
    if (!phrases || phrases.length === 0) return;
    const sample = [...phrases].sort(()=>0.5 - Math.random()).slice(0, 6);
    const items = sample.map((p, i)=>({
      id: p.id + '-' + Date.now() + '-' + i,
      content: p.content,
      top: Math.random()*80 + 5,
      left: Math.random()*60,
      duration: 12 + Math.random()*18,
      delay: Math.random()*6
    }));
    setVisible(items);
  }, [phrases]);

  return (
    <>
      {visible.map(item => (
        <motion.div key={item.id}
          initial={{ x: '-10%', y: item.top + '%', opacity: 0 }}
          animate={{ x: '110%', y: item.top + '%', opacity: [1,1,0.2] }}
          transition={{ duration: item.duration, delay: item.delay, ease: 'linear', repeat: Infinity }}
          style={{ position: 'absolute', top: item.top + '%', left: item.left + '%', pointerEvents: 'none', zIndex: 50 }}
        >
          <div className="px-3 py-2 bg-indigo-600 text-white rounded shadow text-sm" style={{opacity:0.95}}>
            {item.content}
          </div>
        </motion.div>
      ))}
    </>
  );
}
