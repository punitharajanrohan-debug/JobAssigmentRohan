import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserLocation } from '../../Model/auth.models';
import { ItemResponse } from '../../Model/ItemResponse .model';
import { AddBillResult, PurchaseBillRequest, PurchaseBillResponse } from '../../Model/purchase-bill.models';

@Injectable({
  providedIn: 'root',
})
export class PurchaseBill {

  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/purchasebill`;

  getLocations(): Observable<UserLocation[]> {
    return this.http.get<UserLocation[]>(`${this.baseUrl}/locations`);
  }

  getItems(): Observable<ItemResponse[]> {
    return this.http.get<ItemResponse[]>(`${this.baseUrl}/items`);
  }

   getPurchaseBills(): Observable<PurchaseBillResponse[]> {
    return this.http.get<PurchaseBillResponse[]>(`${this.baseUrl}/purchasebills`);
  }

  addPurchaseBill(request: PurchaseBillRequest): Observable<AddBillResult> {
    return this.http.post<AddBillResult>(`${this.baseUrl}/purchasebills`, request);
  }
}
