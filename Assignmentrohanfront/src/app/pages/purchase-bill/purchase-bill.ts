import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PurchaseBill } from '../../Service/PurchaseBill/purchase-bill';
import { UserLocation } from '../../Model/auth.models';
import { ItemResponse } from '../../Model/ItemResponse .model';
import { PurchaseBillRequest, PurchaseBillResponse } from '../../Model/purchase-bill.models';

@Component({
  selector: 'app-purchase-bill',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './purchase-bill.html'
})
export class PurchaseBillComponent implements OnInit {
  private purchaseBillService = inject(PurchaseBill);
  private toastr = inject(ToastrService);

  outerTabs: string[] = ['Header', 'Details', 'Notes', 'Documents'];
  activeOuterTab: string = 'Details';
  innerTabs: string[] = ['Items', 'Accounts', 'Taxes'];
  activeInnerTab: string = 'Items';

  // form fields
  item: string = '';
  batch: number | null = null;   // now holds the location Id
  standardCost: number | null = null;
  standardPrice: number | null = null;
  margin: number | null = null;
  qty: number | null = null;
  freeQty: number | null = null;
  discount: number | null = null;
  totalCost: number = 0;
  totalSelling: number = 0;

  // data
  locations = signal<UserLocation[]>([]);
  items = signal<ItemResponse[]>([]);
  filteredItems = signal<ItemResponse[]>([]);
  showItemSuggestions = signal(false);
  billRows = signal<PurchaseBillResponse[]>([]);
  isSaving = signal(false);

  totalItems = computed(() => this.billRows().length);
  totalQty = computed(() => this.billRows().reduce((sum, row) => sum + row.Qty, 0));

  ngOnInit(): void {
    this.loadItems();
    this.loadLocations();
    this.loadBillRows();
  }

  loadItems(): void {
    this.purchaseBillService.getItems().subscribe({
      next: (data) => {
        this.items.set(data);
        this.filteredItems.set(data);
      },
      error: () => this.toastr.error('Failed to load items.')
    });
  }

  loadLocations(): void {
    this.purchaseBillService.getLocations().subscribe({
      next: (data) => this.locations.set(data),
      error: () => this.toastr.error('Failed to load locations.')
    });
  }

  loadBillRows(): void {
    this.purchaseBillService.getPurchaseBills().subscribe({
      next: (data) => this.billRows.set(data),
      error: () => this.toastr.error('Failed to load purchase bill items.')
    });
  }


  // --- autocomplete ---
 onItemInput(): void {
    const term = this.item.toLowerCase();
    this.filteredItems.set(
      this.items().filter(i => i.Item_Name.toLowerCase().includes(term))
    );
    this.showItemSuggestions.set(true);
  }

   selectItem(name: string): void {
    this.item = name;
    this.showItemSuggestions.set(false);
  }

  // --- live calculation ---
  calculateTotals(): void {
    const cost = this.standardCost ?? 0;
    const price = this.standardPrice ?? 0;
    const quantity = this.qty ?? 0;
    const discountPct = this.discount ?? 0;

    const grossCost = cost * quantity;
    this.totalCost = grossCost - (grossCost * discountPct / 100);
    this.totalSelling = price * quantity;
  }

  // --- Add button ---
  addBill(): void {
    const selectedItem = this.items().find(
      i => i.Item_Name.toLowerCase() === this.item.trim().toLowerCase()
    );

    if (!selectedItem) {
      this.toastr.warning('Please select a valid item from the list.', 'Validation');
      return;
    }
    if (this.batch == null) {
      this.toastr.warning('Please select a batch.', 'Validation');
      return;
    }
    if (!this.qty || this.qty <= 0) {
      this.toastr.warning('Quantity must be greater than 0.', 'Validation');
      return;
    }
    if ((this.discount ?? 0) < 0 || (this.discount ?? 0) > 100) {
      this.toastr.warning('Discount must be between 0 and 100.', 'Validation');
      return;
    }

    const request: PurchaseBillRequest = {
      Item_Id: selectedItem.Id,
      Location_Id: Number(this.batch),
      Standard_Cost: this.standardCost ?? 0,
      Standard_Price: this.standardPrice ?? 0,
      Margin: this.margin ?? 0,
      Qty: this.qty,
      Free_Qty: this.freeQty ?? 0,
      Discount: this.discount ?? 0,
      Total_Cost: this.totalCost,
      Total_Selling: this.totalSelling
    };

    this.isSaving.set(true);

    this.purchaseBillService.addPurchaseBill(request).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.toastr.success('Item added to the bill.');
        this.resetForm();
        this.loadBillRows();
      },
      error: (err) => {
        this.isSaving.set(false);
        this.toastr.error(err?.error?.Message || 'Failed to add the item.', 'Error');
      }
    });
  }

  resetForm(): void {
    this.item = '';
    this.batch = null;
    this.standardCost = null;
    this.standardPrice = null;
    this.margin = null;
    this.qty = null;
    this.freeQty = null;
    this.discount = null;
    this.totalCost = 0;
    this.totalSelling = 0;
    this.filteredItems.set(this.items());
  }
}