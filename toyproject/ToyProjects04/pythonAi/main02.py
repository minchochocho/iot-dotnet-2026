# FastAPI Server 02
import uvicorn
from fastapi import FastAPI, HTTPException

app = FastAPI() # 객체 생성

# json 데이터 생성
products = [
    {
        'id':1,
        'name':'Lenovo AI 154',
        'price':2_500_000   # 25000000 
    },
    {
        'id':2,
        'name':'MS Bluetooth',
        'price':120_000   # 25000000 
    }
]

# 기본 route
@app.get('/')
def root():
    return{
        'message' : "FastAPI server start!"
    }

# 전체 데이터
@app.get('/products')
def get_product():
    return products

# 상세   데이터
@app.get('/products/{product_id}')
def get_product(product_id: int):
    for product in products:
        if product['id'] == product_id:
            return product
        
    raise HTTPException(status_code=404,
                        detail='제품을 찾을 수 없습니다.')

# 디버깅을 하고 싶으면 uvicorn을 사용
if __name__ == '__main__':
    uvicorn.run('main02:app', host='127.0.0.1', port=8000, reload=True)